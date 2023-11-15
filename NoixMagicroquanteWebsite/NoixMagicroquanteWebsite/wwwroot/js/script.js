﻿// Code permettant de gerer la creation d'un utilisateur dans la partie administrative
var createUserForm = document.getElementById('createUserForm');
if (createUserForm) {
    createUserForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        // Créer un objet FormData à partir du formulaire
        var formData = new FormData(createUserForm);

        // Validez le formulaire
        if (!await validateForm(formData, null)) {
            return; // Stoppez la soumission du formulaire si la validation échoue
        }

        // Envoi de la requête HTTP à AccountController
        fetch('/account/signup', {
            method: 'POST',
            body: formData
        })
        .then(data => {
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

// Fonctions pour vérifier l'email de manière asynchrone
async function checkEmail(email) {
    const response = await fetch('/account/checkemail?email=' + encodeURIComponent(email));
    const isEmailTaken = await response.json();
    return !isEmailTaken;
}

async function checkEmailWithId(email, id) {
    const response = await fetch('/account/checkemailwithid?email=' + encodeURIComponent(email) + '&id=' + encodeURIComponent(id));
    const isEmailTaken = await response.json();
    return !isEmailTaken;
}

async function validateForm(formData, UserId) {
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
                    if (UserId == null)
                        document.getElementById('FirstNameError').innerHTML = 'Le prénom est requis.';
                    else
                        document.getElementById('editFirstNameError').innerHTML = 'Le prénom est requis.';
                    break;
                case 'LastName':
                    if (UserId == null)
                        document.getElementById('LastNameError').innerHTML = 'Le nom est requis.';
                    else
                        document.getElementById('editLastNameError').innerHTML = 'Le nom est requis.';
                    break;
                case 'UserName':
                    if (UserId == null)
                        document.getElementById('UserNameError').innerHTML = 'Le nom d\'utilisateur est requis.';
                    else
                        document.getElementById('editUserNameError').innerHTML = 'Le nom d\'utilisateur est requis.';
                    break;
                case 'Email':
                    if (UserId == null)
                        document.getElementById('EmailError').innerHTML = 'L\'adresse courriel est requise.';
                    else
                        document.getElementById('editEmailError').innerHTML = 'L\'adresse courriel est requise.';
                    break;
                case 'Password':
                    if (UserId == null)
                        document.getElementById('PasswordError').innerHTML = 'Le mot de passe est requis.';
                    else
                        document.getElementById('editPasswordError').innerHTML = 'Le mot de passe est requis.';
                    break;
                case 'ConfirmPassword':
                    if (UserId == null)
                        document.getElementById('ConfirmPasswordError').innerHTML = 'La confirmation du mot de passe est requise.';
                    else
                        document.getElementById('editConfirmPasswordError').innerHTML = 'La confirmation du mot de passe est requise.';
                    break;
            }
        }
    }

    if (UserId == null) {
        // Vérifiez si l'adresse courriel est déjà utilisée
        if (formData.get('Email') && !await checkEmail(formData.get('Email'))) {
            isValid = false;
            document.getElementById('EmailError').innerHTML = 'Cette adresse courriel est déjà utilisée.';
        }
    }
    else
    {
        // Vérifiez si l'adresse courriel est déjà utilisée
        if (formData.get('Email') && !await checkEmailWithId(formData.get('Email'), UserId)) {
            isValid = false;
            document.getElementById('editEmailError').innerHTML = 'Cette adresse courriel est déjà utilisée.';
        }
    }

    // Vérifiez que les mots de passe correspondent
    if (formData.get('Password') && formData.get('Password') !== formData.get('ConfirmPassword')) {
        isValid = false;
        document.getElementById('ConfirmPasswordError').innerHTML = 'Les mots de passe ne correspondent pas.';
    }

    return isValid;
}

// Code permettant de gerer la modification d'un utilisateur dans la partie administrative
var editBtn = document.querySelectorAll('.editBtn');
editBtn.forEach(function (btn) {
    btn.addEventListener('click', function (event) {
        var UserId = parseInt(btn.parentNode.parentNode.childNodes[1].innerText, 10);
        var FirstName = btn.parentNode.parentNode.childNodes[3].innerText;
        var LastName = btn.parentNode.parentNode.childNodes[5].innerText;
        var UserName = btn.parentNode.parentNode.childNodes[7].innerText;
        var Email = btn.parentNode.parentNode.childNodes[9].innerText;
        var IsAdmin = btn.parentNode.parentNode.childNodes[11].innerText;

        document.getElementById('editUserId').value = UserId;
        document.getElementById('editFirstName').value = FirstName;
        document.getElementById('editLastName').value = LastName;
        document.getElementById('editUserName').value = UserName;
        document.getElementById('editEmail').value = Email;
        document.getElementById('editIsAdmin').value = IsAdmin === 'Oui' ? true : false;
    });
});

var editUserForm = document.getElementById('editUserForm');
if (editUserForm) {
    editUserForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        // Créer un objet FormData à partir du formulaire
        var formData = new FormData(editUserForm);

        // Validez le formulaire
        if (!await validateForm(formData, formData.get('UserId'))) {
            return; // Stoppez la soumission du formulaire si la validation échoue
        }

        // Envoi de la requête HTTP à AccountController
        fetch('/account/edituser', {
            method: 'POST',
            body: formData
        })
        .then(data => {
            $('#editUserModal').modal('hide');
            window.location.reload();
        })
        .catch(error => {
            console.error('Erreur:', error);
        });

        $('#editUserModal').on('show.bs.modal', function (event) {
            // Effacer les messages d'erreur lors de l'ouverture de la modale
            document.getElementById('editFirstNameError').innerHTML = '';
            document.getElementById('editLastNameError').innerHTML = '';
            document.getElementById('editUserNameError').innerHTML = '';
            document.getElementById('editEmailError').innerHTML = '';
            document.getElementById('editPasswordError').innerHTML = '';
            document.getElementById('editConfirmPasswordError').innerHTML = '';
        });
    })
}

// Code permettant de gerer la modification d'un utilisateur dans la partie compte
var editAccountForm = document.getElementById('editAccountForm');
if (editAccountForm) {
    //a faire
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