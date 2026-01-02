// Initialize tooltips
$(function () {
    $('[data-bs-toggle="tooltip"]').tooltip();
});

// Auto-dismiss alerts
setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

// Form validation enhancements
$(document).ready(function () {
    $('form').on('submit', function () {
        $('button[type="submit"]').prop('disabled', true).html('<i class="fas fa-spinner fa-spin me-1"></i>Processing...');
    });
});