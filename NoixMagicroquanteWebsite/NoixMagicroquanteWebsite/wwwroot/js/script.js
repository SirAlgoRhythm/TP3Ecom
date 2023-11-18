function togglePasswordVisibility() {
    var passwordInput = document.getElementById('Password');
    var passwordIcon = document.getElementById('passwordIcon');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordIcon.classList.add('fa-eye-slash');
        passwordIcon.classList.remove('fa-eye');
    }
    else {
        passwordInput.type = 'password';
        passwordIcon.classList.add('fa-eye');
        passwordIcon.classList.remove('fa-eye-slash');
    }
}

function toggleAccountEdit() {
    event.preventDefault(); // Empêche le formulaire de se soumettre

    var hiddenFields = document.getElementById('hiddenFields');

    // Basculer la classe 'active' pour contrôler l'affichage
    if (hiddenFields.classList.contains('active')) {
        hiddenFields.classList.remove('active');
    } else {
        hiddenFields.classList.add('active');
    }

    // Basculer l'attribut disabled des champs si nécessaire
    var fieldsToToggle = ['FirstName', 'LastName', 'UserName', 'Email'];
    fieldsToToggle.forEach(function (fieldId) {
        var field = document.getElementById(fieldId);
        if (field) {
            field.disabled = !field.disabled;
        }
    });

    // Vous pouvez également basculer le texte du bouton Éditer si nécessaire
    var editButton = document.getElementById('btnEdit');
    var saveButton = document.getElementById('btnSave');
    var cancelButton = document.getElementById('btnCancel');

    if (hiddenFields.classList.contains('active')) {
        editButton.style.display = 'none';
        saveButton.style.display = 'inline-block';
        cancelButton.style.display = 'inline-block';
    } else {
        editButton.style.display = 'inline-block';
        saveButton.style.display = 'none';
        cancelButton.style.display = 'none';
    }
}