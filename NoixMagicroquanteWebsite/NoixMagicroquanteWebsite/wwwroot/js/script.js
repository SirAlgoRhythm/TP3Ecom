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
    // Empêche le formulaire de se soumettre
    event.preventDefault();

    // Basculer la visibilité des champs cachés
    var hiddenFields = document.getElementById('hiddenFields');
    hiddenFields.style.display = hiddenFields.style.display === 'none' ? '' : 'none';

    // Basculer l'attribut disabled
    document.getElementById('FirstName').disabled = !document.getElementById('FirstName').disabled;
    document.getElementById('LastName').disabled = !document.getElementById('LastName').disabled;
    document.getElementById('UserName').disabled = !document.getElementById('UserName').disabled;
    document.getElementById('Email').disabled = !document.getElementById('Email').disabled;

    // Vous pouvez également basculer le texte du bouton Éditer si nécessaire
    var editButton = document.getElementById('btnEdit');
    if (hiddenFields.style.display === 'none') {
        editButton.style.display = '';
    } else {
        editButton.style.display = 'none';
    }
}