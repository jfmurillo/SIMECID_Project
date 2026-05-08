function ProfileData() {
    this.ViewName = "User";
    this.ApiService = "User";
    var email;

    this.InitView = function () {


        // Obtener el email de la URL
        let urlParams = new URLSearchParams(window.location.search);
        email = urlParams.get(`email`);


        // Llamar al método para obtener y actualizar la información del usuario
        this.RetrieveUserByEmail(email);
    };

    this.RetrieveUserByEmail = function (email) {
        let route = "User/RetrieveUserByEmail?email=" + email;
        let ca = new ControlActions();

        ca.GetToApi(route, function (response) {



            // Verificar si los datos están disponibles y no son undefined
            if (response) {
                // Establecer la ruta de la imagen del usuario desde la respuesta
                let imageName = response.imageName;
                let profileIcon = document.querySelector('.userIcon');
                profileIcon.src = "/ProfilePictureUploads/" + imageName;
            } else {

            }

        }, function (error) {

        });
    }
}

$(document).ready(function () {
    var pd = new ProfileData();
    pd.InitView();
})