function validarFormatoFecha(fecha) {
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((fecha.match(RegExPattern)) && (fecha != '')) {
        var fechaf = fecha.trim().split("/");
        var d = fechaf[0];
        var m = fechaf[1];
        var y = fechaf[2];
        return m > 0 && m < 13 && y > 0 && y < 32768 && d > 0 && d <= (new Date(y, m, 0)).getDate();
    }
    return false;
}

function limpiarTabla(aCuerpoTabla, aFooterTabla) {
    var pCuerpoTabla = document.getElementById(aCuerpoTabla);
    var pFooterTabla = document.getElementById(aFooterTabla);

    while (pCuerpoTabla.hasChildNodes()) {
        pCuerpoTabla.removeChild(pCuerpoTabla.firstChild);
    }

    if (pFooterTabla != null) {
        if (pFooterTabla.hasChildNodes()) {
            pFooterTabla.removeChild(pFooterTabla.firstChild);
        }
    }        
}

function zeroPad(num, places) {
    var zero = places - num.toString().length + 1;
    return Array(+(zero > 0 && zero)).join("0") + num;
}

function cambiarEstadoInput(pCheckBox, AClave) {//Habilita o desabilita un input al marcar o desmarcar el checkbox
    var pInput = document.getElementById("inp" + AClave);

    if (!pCheckBox.checked) {
        pInput.disabled = "disabled";
        pInput.value = "";
    } else {
        pInput.removeAttribute("disabled");
        pInput.focus();
    }
}

function cerrarCaptura() {//Cierra la ventana modal.
    $(".ui-dialog-titlebar-close").trigger('click');
}