$(document).ready(function () {
    $(".card-body").LoadingOverlay("show");
    fetch("/Negocio/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {

            console.log(responseJson)

            if (responseJson.estado) {
                const obj = responseJson.objeto
                $("txtNumeroDocumento").val(obj.numeroDocumento)
                $("txtRazonSocial").val(obj.nombre)
                $("txtCorreo").val(obj.correo)
                $("txtDireccion").val(obj.direccion)
                $("txTelefono").val(obj.telefono)
                $("txtImpuesto").val(obj.porcentajeImpuesto)
                $("txtSimboloMoneda").val(obj.simboloMoneda)
                $("imgLogo").attr("src", obj.urlLogo)
            } else {
                swal("Lo sentimos",responseJson.mensaje,"error")
            }
        })
})