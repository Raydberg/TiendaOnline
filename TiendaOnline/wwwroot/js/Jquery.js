$(document).ready(function () {
    $('#marcaDropdown').change(function () {
        var marcaId = $(this).val();

        $("#modeloDropdown").empty().append('<option value="">Seleccione Modelo</option>').prop("disabled", true);

        if (marcaId) {
            $.ajax({
                url: '/Productos/ObtenerModelos',
                type: 'GET',
                data: { idMarca: marcaId },
                success: function (data) {
                    $.each(data, function (i, modelo) {
                        $('#modeloDropdown').append('<option value="' + modelo.idModelo + '">' + modelo.noM_MODELO + '</option>');
                    });
                    $("#modeloDropdown").prop("disabled", false);
                }

            });
        }
    });
})
//$(document).ready(function () {

//    $("#marcaDropdown").change(function () {
//        var marcaID = $(this).val();
//        console.log("idMarca : " + marcaID)

//    })

//})