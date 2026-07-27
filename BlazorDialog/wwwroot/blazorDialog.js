var blazorDialog = function (blazorDialog) {

    var openDialogs = [];
    var isKeyUpListenerRegistered = false;
    var keyUpEventListener = function (keyboardEventArgs) {
        if (openDialogs.length > 0) {
            var currentDialog = openDialogs[openDialogs.length - 1];
            if (currentDialog.isKeyboarCloseEnabled && keyboardEventArgs.key == currentDialog.key) {
                currentDialog.dialog.invokeMethodAsync("HideFromKeyPress");
            }
        }
    }

    blazorDialog.registerShownDialog = function (dialogReference, isKeyboarCloseEnabled, key) {
        openDialogs.push({ dialog: dialogReference, isKeyboarCloseEnabled: isKeyboarCloseEnabled, key: key });
        if (!isKeyUpListenerRegistered) {
            isKeyUpListenerRegistered = true;
            document.addEventListener("keyup", keyUpEventListener);
        }
    }

    blazorDialog.unregisterShownDialog = function (dialogReference) {
        var found = openDialogs.find(function (dialogRef) {
            return dialogRef.dialog._id == dialogReference._id;
        });
        if (found) {
            openDialogs.splice(openDialogs.indexOf(found), 1);
        }

        if (openDialogs.length == 0) {
            document.removeEventListener("keyup", keyUpEventListener);
            isKeyUpListenerRegistered = false;
        }

    }

    /* ============== Dragging ============== */

    var customDragHandleSelector = ".blazor-dialog-drag-handle";
    var defaultDragHandleSelector = ".blazor-dialog-header";
    var nonDraggableTargetsSelector = "button, a, input, select, textarea, label, [contenteditable]";

    // Finds the element that starts a drag. A custom handle wins over the default header. Elements
    // that belong to a nested dialog are ignored, so a dialog never gets dragged by its child's header.
    var findDragHandle = function (wrapperElement) {
        var candidates = wrapperElement.querySelectorAll(customDragHandleSelector + ", " + defaultDragHandleSelector);
        var defaultHandle = null;
        for (var i = 0; i < candidates.length; i++) {
            var candidate = candidates[i];
            if (candidate.closest(".blazor-dialog-content-wrapper") !== wrapperElement) {
                continue;
            }
            if (candidate.matches(customDragHandleSelector)) {
                return candidate;
            }
            if (defaultHandle === null) {
                defaultHandle = candidate;
            }
        }
        return defaultHandle;
    }

    var clamp = function (value, boundA, boundB) {
        var min = Math.min(boundA, boundB);
        var max = Math.max(boundA, boundB);
        return Math.min(Math.max(value, min), max);
    }

    blazorDialog.enableDragging = function (wrapperElement) {
        // guard against anything that is not a live element, so that a bad call can never take down
        // the whole blazor renderer
        if (!wrapperElement || typeof wrapperElement.querySelectorAll !== "function" || wrapperElement.blazorDialogDrag) {
            return;
        }
        var handle = findDragHandle(wrapperElement);
        if (!handle) {
            return;
        }

        var state = { handle: handle, offsetX: 0, offsetY: 0, pointerId: null };
        wrapperElement.blazorDialogDrag = state;

        handle.style.cursor = "move";
        handle.style.touchAction = "none";
        handle.style.userSelect = "none";

        state.onPointerDown = function (e) {
            if (state.pointerId !== null || (e.pointerType === "mouse" && e.button !== 0)) {
                return;
            }
            if (e.target.closest && e.target.closest(nonDraggableTargetsSelector)) {
                return;
            }

            // The rect already includes the applied translation, remove it to get the untranslated position.
            var rect = wrapperElement.getBoundingClientRect();
            var left = rect.left - state.offsetX;
            var top = rect.top - state.offsetY;

            state.minX = -left;
            state.maxX = window.innerWidth - rect.width - left;
            state.minY = -top;
            state.maxY = window.innerHeight - rect.height - top;
            state.startX = e.clientX;
            state.startY = e.clientY;
            state.originX = state.offsetX;
            state.originY = state.offsetY;
            state.pointerId = e.pointerId;

            handle.setPointerCapture(e.pointerId);
            e.preventDefault();
        }

        state.onPointerMove = function (e) {
            if (state.pointerId !== e.pointerId) {
                return;
            }
            state.offsetX = clamp(state.originX + e.clientX - state.startX, state.minX, state.maxX);
            state.offsetY = clamp(state.originY + e.clientY - state.startY, state.minY, state.maxY);
            wrapperElement.style.transform = "translate(" + state.offsetX + "px, " + state.offsetY + "px)";
        }

        state.onPointerUp = function (e) {
            if (state.pointerId !== e.pointerId) {
                return;
            }
            state.pointerId = null;
            if (handle.hasPointerCapture(e.pointerId)) {
                handle.releasePointerCapture(e.pointerId);
            }
        }

        handle.addEventListener("pointerdown", state.onPointerDown);
        handle.addEventListener("pointermove", state.onPointerMove);
        handle.addEventListener("pointerup", state.onPointerUp);
        handle.addEventListener("pointercancel", state.onPointerUp);
    }

    blazorDialog.disableDragging = function (wrapperElement) {
        var state = wrapperElement ? wrapperElement.blazorDialogDrag : null;
        if (!state) {
            return;
        }

        state.handle.removeEventListener("pointerdown", state.onPointerDown);
        state.handle.removeEventListener("pointermove", state.onPointerMove);
        state.handle.removeEventListener("pointerup", state.onPointerUp);
        state.handle.removeEventListener("pointercancel", state.onPointerUp);

        state.handle.style.cursor = "";
        state.handle.style.touchAction = "";
        state.handle.style.userSelect = "";
        wrapperElement.style.transform = "";
        wrapperElement.blazorDialogDrag = null;
    }


    return blazorDialog;
}({});







