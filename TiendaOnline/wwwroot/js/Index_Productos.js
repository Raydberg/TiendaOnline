$(document).ready(function () {
    $('.delete-btn').click(function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        // Asumiendo que estás en un entorno Razor, necesitarías pasar esta URL de alguna otra manera.
        var url = '/Productos/Delete/' + id; // Modifica según sea necesario
        $('#deleteConfirmed').attr('href', url);
        $('#confirmDeleteModal').modal('show');
    });

    $('#cancelBtn').click(function () {
        $('#confirmDeleteModal').modal('hide');
    });

    // Modifica aquí para incluir la redirección después de la eliminación
    $('#deleteConfirmed').click(function () {
        var baseUrl = $(this).data('base-url'); // Obtiene la URL base del atributo de datos
        // Aquí deberías tener tu lógica de eliminación (por ejemplo, una llamada AJAX)
        // Después de la eliminación exitosa, redirige al usuario
        window.location.href = baseUrl; // Redirige al usuario al Index del controlador Productos
    });
});
