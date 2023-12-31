﻿// Fonctions pour vérifier l'email de manière asynchrone
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

    if (UserId == null) {
        // Effacez d'abord tous les messages d'erreur précédents
        document.getElementById('FirstNameError').innerHTML = '';
        document.getElementById('LastNameError').innerHTML = '';
        document.getElementById('UserNameError').innerHTML = '';
        document.getElementById('EmailError').innerHTML = '';
        document.getElementById('PasswordError').innerHTML = '';
        document.getElementById('ConfirmPasswordError').innerHTML = '';
    }
    else {
        document.getElementById('editFirstNameError').innerHTML = '';
        document.getElementById('editLastNameError').innerHTML = '';
        document.getElementById('editUserNameError').innerHTML = '';
        document.getElementById('editEmailError').innerHTML = '';
        document.getElementById('editPasswordError').innerHTML = '';
        document.getElementById('editConfirmPasswordError').innerHTML = '';
        if (document.getElementById('NewPassword'))
            document.getElementById('editNewPasswordError').innerHTML = '';
    }

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
                case 'NewPassword':
                    document.getElementById('editNewPasswordError').innerHTML = 'Le nouveau mot de passe est requis.';
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
    if (document.getElementById('NewPassword') && document.getElementById('NewPassword').value != '') {
        if (formData.get('NewPassword') && formData.get('NewPassword') !== formData.get('ConfirmPassword')) {
            isValid = false;
            document.getElementById('editConfirmPasswordError').innerHTML = 'Les nouveaux mots de passe ne correspondent pas.';
        }
    }
    else {
        if (formData.get('Password') && formData.get('Password') !== formData.get('ConfirmPassword')) {
            isValid = false;
            document.getElementById('editConfirmPasswordError').innerHTML = 'Les mots de passe ne correspondent pas.';
        }
    }

    return isValid;
}

// Code permettant de gerer la creation d'un utilisateur dans la partie administrative
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

// Code permettant de gerer la modification d'un utilisateur dans la partie administrative
var editBtn = document.querySelectorAll('.editBtn');
editBtn.forEach(function (btn) {
    btn.addEventListener('click', function (event) {
        var tr = btn.closest('tr'); // Trouver la ligne du tableau
        var UserId = tr.querySelector('.userId').textContent;
        var FirstName = tr.querySelector('.firstName').textContent;
        var LastName = tr.querySelector('.lastName').textContent;
        var UserName = tr.querySelector('.userName').textContent;
        var Email = tr.querySelector('.email').textContent;
        var IsAdmin = tr.querySelector('.isAdmin').textContent;

        document.getElementById('editUserId').value = UserId;
        document.getElementById('editFirstName').value = FirstName;
        document.getElementById('editLastName').value = LastName;
        document.getElementById('editUserName').value = UserName;
        document.getElementById('editEmail').value = Email;
        document.getElementById('editIsAdmin').value = IsAdmin === 'Oui';
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
    });

    $('#editUserModal').on('show.bs.modal', function (event) {
        editUserForm.reset();

        // Effacer les messages d'erreur lors de l'ouverture de la modale
        document.getElementById('editFirstNameError').innerHTML = '';
        document.getElementById('editLastNameError').innerHTML = '';
        document.getElementById('editUserNameError').innerHTML = '';
        document.getElementById('editEmailError').innerHTML = '';
        document.getElementById('editPasswordError').innerHTML = '';
        document.getElementById('editConfirmPasswordError').innerHTML = '';
    });
}

// Code permettant de gerer la modification d'un utilisateur dans la partie compte
var editAccountForm = document.getElementById('editAccountForm');
if (editAccountForm) {
    editAccountForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        // Créer un objet FormData à partir du formulaire
        var formData = new FormData(editAccountForm);

        // Validez le formulaire
        if (!await validateForm(formData, formData.get('UserId'))) {
            return; // Stoppez la soumission du formulaire si la validation échoue
        }

        // Envoi de la requête HTTP à AccountController
        fetch('/account/editaccount', {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erreur réseau');
            }
            return response.json(); // Convertir la réponse en JSON
        })
        .then(data => {
            if (!data.success) {
                // Affichez le message d'erreur dans l'élément approprié
                document.getElementById('editPasswordError').textContent = data.message;
            } else {
                window.location.reload();
            }
        })
        .catch(error => {
            console.error('Erreur:', error);
        });
    });
}

document.querySelectorAll('.deleteBtn').forEach(button => {
    button.addEventListener('click', function () {
        var userId = this.getAttribute('data-user-id');
        var deleteUrl = 'delete?UserId=' + userId;
        document.getElementById('deleteUserLink').setAttribute('href', deleteUrl);
    });
});

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
        setTimeout(function () {
            window.location.reload();
        }, 500);
    }
}

function toggleSearchBar() {
    var searchBar = document.getElementById('searchBar');
    var chevron = document.getElementById('chevron');

    function handleTransitionEnd(event) {
        // Vérifier si la transition de max-width est terminée
        if (event.propertyName === 'max-width' && !searchBar.classList.contains('active')) {
            searchBar.removeEventListener('transitionend', handleTransitionEnd);
            searchBar.querySelector('input').value = ''; // Réinitialiser l'input
        }
    }

    if (searchBar.classList.contains('active')) {
        chevron.style.transform = 'rotate(0deg)';
        searchBar.style.maxWidth = '0';
        setTimeout(function () {
            searchBar.classList.remove('active');
            searchBar.querySelector('input').value = ''; // Réinitialiser l'input
            window.location.reload();
        }, 500);
    } else {
        searchBar.classList.add('active');
        chevron.style.transform = 'rotate(180deg)';
        searchBar.style.maxWidth = '80%';
    }
}

var searchBar = document.getElementById('searchBar');
if (searchBar) {
    var searchInput = searchBar.querySelector('input');
    var searchSelect = searchBar.querySelector('select');
    var debounceTimeout;

    function fetchAndDisplayUsers() {
        var searchValue = searchInput.value.toLowerCase();
        var isAdmin = null;
        if (searchSelect.value === "1") {
            isAdmin = true;
        } else if (searchSelect.value === "0") {
            isAdmin = false;
        }

        var url;
        if (searchValue.length === 0) {
            url = '/account/getusersbyisadmin?isAdmin=' + encodeURIComponent(isAdmin);
        } else if (!isNaN(Number(searchValue))) {
            url = '/account/getuserbyid?id=' + encodeURIComponent(Number(searchValue)) + '&isAdmin=' + encodeURIComponent(isAdmin);
        } else {
            url = '/account/getuserslikestring?searchString=' + encodeURIComponent(searchValue) + '&isAdmin=' + encodeURIComponent(isAdmin);
        }

        fetch(url, { method: 'GET' })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Erreur réseau');
                }
                return response.json();
            })
            .then(data => {
                if (data.length > 0) {
                    createTbodyUsers(data);
                } else {
                    createTbodyEmpty();
                }
            })
            .catch(error => {
                console.error('Erreur:', error);
            });
    }

    searchSelect.addEventListener('change', fetchAndDisplayUsers);

    searchInput.addEventListener('input', function () {
        clearTimeout(debounceTimeout);
        debounceTimeout = setTimeout(fetchAndDisplayUsers, 500);
    });
}

function createTbodyEmpty() {
    var tbody = document.getElementById('tbodyUsers');
    tbody.innerHTML = '';
    var tr = document.createElement('tr');
    tr.innerHTML = `
        <td colspan="7" class="text-center">Aucun utilisateur trouvé</td>
    `;
    tbody.appendChild(tr);
}

function createTbodyUsers(users) {
    var tbody = document.getElementById('tbodyUsers');
    tbody.innerHTML = '';

    users.forEach(user => {
        var isAdmin = user.isAdmin ? "Oui" : "Non";
        var tr = document.createElement('tr');
        tr.className = 'align-middle';
        tr.innerHTML = `
            <th class="userId" scope="row">${user.userId}</th>
            <td class="firstName">${user.firstName}</td>
            <td class="lastName">${user.lastName}</td>
            <td class="userName">${user.userName}</td>
            <td class="email">${user.email}</td>
            <td class="isAdmin">${isAdmin}</td>
            <td>
                <button type="button" class="btn btn-primary editBtn" data-user-id="${user.userId}" data-bs-toggle="modal" data-bs-target="#editUserModal"><i class="fas fa-edit"></i></button>
                <button type="button" class="btn btn-danger deleteBtn" data-user-id="${user.userId}" data-bs-toggle="modal" data-bs-target="#deleteUserModal"><i class="fas fa-trash"></i></button>
            </td>
        `;
        tbody.appendChild(tr);
    });
    attachEditButtonEventListeners();
    attachDeleteButtonEventListeners();
}
function attachEditButtonEventListeners() {
    document.querySelectorAll('.editBtn').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var tr = btn.closest('tr');
            var UserId = tr.querySelector('.userId').textContent;
            var FirstName = tr.querySelector('.firstName').textContent;
            var LastName = tr.querySelector('.lastName').textContent;
            var UserName = tr.querySelector('.userName').textContent;
            var Email = tr.querySelector('.email').textContent;
            var IsAdmin = tr.querySelector('.isAdmin').textContent;

            document.getElementById('editUserId').value = UserId;
            document.getElementById('editFirstName').value = FirstName;
            document.getElementById('editLastName').value = LastName;
            document.getElementById('editUserName').value = UserName;
            document.getElementById('editEmail').value = Email;
            document.getElementById('editIsAdmin').value = IsAdmin === 'Oui';
        });
    });
}

function attachDeleteButtonEventListeners() {
    document.querySelectorAll('.deleteBtn').forEach(button => {
        button.addEventListener('click', function () {
            var userId = this.getAttribute('data-user-id');
            var deleteUrl = 'delete?UserId=' + userId;
            document.getElementById('deleteUserLink').setAttribute('href', deleteUrl);
        });
    });
}