var createUserForm = document.getElementById('createUserForm');
if (createUserForm) {
    createUserForm.addEventListener('submit', function (event) {
        event.preventDefault();

        // Créer un objet FormData à partir du formulaire
        var formData = new FormData(createUserForm);

        // Validez le formulaire
        if (!validateForm(formData)) {
            return; // Stoppez la soumission du formulaire si la validation échoue
        }

        // Remplacez 'votre_endpoint_pour_creer_utilisateur' par l'URL de votre API
        fetch('/account/signup', {
            method: 'POST',
            body: formData
        })
        .then(data => {
            console.log(data);
            // Gérez la réponse ici, par exemple en fermant la fenêtre modale
            // et en mettant à jour l'affichage pour montrer le nouvel utilisateur
            $('#createUserModal').modal('hide');
            window.location.reload();
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
    });

    $('#createUserModal').on('show.bs.modal', function (event) {
        createUserForm.reset();

        // Effacer les messages d'erreur lors de l'ouverture de la modale
        document.getElementById('FirstNameError').innerHTML = '';
        document.getElementById('LastNameError').innerHTML = '';
        document.getElementById('UserNameError').innerHTML = '';
        document.getElementById('EmailError').innerHTML = '';
        document.getElementById('PasswordError').innerHTML = '';
        document.getElementById('ConfirmPasswordError').innerHTML = '';
    });
}

function validateForm(formData) {
    let isValid = true;

    // Effacez d'abord tous les messages d'erreur précédents
    document.getElementById('FirstNameError').innerHTML = '';
    document.getElementById('LastNameError').innerHTML = '';
    document.getElementById('UserNameError').innerHTML = '';
    document.getElementById('EmailError').innerHTML = '';
    document.getElementById('PasswordError').innerHTML = '';
    document.getElementById('ConfirmPasswordError').innerHTML = '';

    // Vérifiez que tous les champs requis sont remplis
    for (let [key, value] of formData.entries()) {
        if (!value) {
            isValid = false;
            switch (key) {
                case 'FirstName':
                    document.getElementById('FirstNameError').innerHTML = 'Le prénom est requis.';
                    break;
                case 'LastName':
                    document.getElementById('LastNameError').innerHTML = 'Le nom est requis.';
                    break;
                case 'UserName':
                    document.getElementById('UserNameError').innerHTML = 'Le nom d\'utilisateur est requis.';
                    break;
                case 'Email':
                    document.getElementById('EmailError').innerHTML = 'L\'adresse courriel est requise.';
                    break;
                case 'Password':
                    document.getElementById('PasswordError').innerHTML = 'Le mot de passe est requis.';
                    break;
                case 'ConfirmPassword':
                    document.getElementById('ConfirmPasswordError').innerHTML = 'La confirmation du mot de passe est requise.';
                    break;
            }
        }
    }

    // Vérifiez que les mots de passe correspondent
    if (formData.get('Password') && formData.get('Password') !== formData.get('ConfirmPassword')) {
        isValid = false;
        document.getElementById('ConfirmPasswordError').innerHTML = 'Les mots de passe ne correspondent pas.';
    }

    return isValid;
}

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