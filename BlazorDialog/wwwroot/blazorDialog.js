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


    return blazorDialog;
}({});







