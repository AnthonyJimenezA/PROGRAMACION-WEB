// wwwroot/js/site.js

// Función para inicializar alertas Bootstrap
function initializeAlerts() {
    const alertList = document.querySelectorAll('.alert-dismissible');
    alertList.forEach(function (alert) {
        new bootstrap.Alert(alert);
    });
}

document.addEventListener('DOMContentLoaded', function () {
    initializeAlerts();
});
