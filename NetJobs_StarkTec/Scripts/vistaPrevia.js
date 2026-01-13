function mostrarVistaPrevia(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var img = document.getElementById('<%= imgVistaPrevia.ClientID %>');
            img.src = e.target.result;
            img.style.display = 'block'; // Mostrar la imagen
        };
        reader.readAsDataURL(input.files[0]);
    }
}
