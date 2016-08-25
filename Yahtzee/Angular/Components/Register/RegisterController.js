function RegisterController(loginService) {
    var ctrl = this;

    ctrl.Register = function () {
        console.log("User clicked register");
        if (ctrl.registerModel.password === ctrl.registerModel.repeatPassword && ctrl.registerModel.username !== '' && ctrl.registerModel.password.length > 6) {
            var userRegistrationInfo = {
                Email: ctrl.registerModel.username,
                Password: ctrl.registerModel.password,
                ConfirmPassword: ctrl.registerModel.repeatPassword
            };
            console.log("Valid registration form. Trying to register");
            var promiseregister = loginService.register(userRegistrationInfo);

            promiseregister.then(function(resp) {
                console.log("Registered successfully!");
                    alert("Your account has been successfully registered! You can now login to your account to start playing!");
                    ctrl.Reset();
                },
                function(err) {
                    console.log("Register request unsuccessfull");
                    console.log(err.data.ModelState[""][0]);
                    alert("Failed to register: \nReason: " + err.data.ModelState[""][0]);
                });
        } else {
            if (ctrl.registerModel.password !== ctrl.registerModel.repeatPassword && ctrl.registerModel.password.length > 6) {
                alert("Make sure both passwords are the same");
            }
            if (ctrl.registerModel.username === "") {
                alert("You need to enter your email adress");
            }

            if (ctrl.registerModel.password.length < 6 && ctrl.registerModel.username !== "") {
                alert("Your password must contain at least 6 characters!");
            }
            
        }

    };
    ctrl.Reset = function() {
        ctrl.registerModel = {
            username: '',
            password: '',
            repeatPassword: ''
        };
    };
    ctrl.Reset();
}

angular.module('Yahtzee')
    .component('register',
    {
        controller: RegisterController,
        templateUrl: '/Angular/Components/Register/register.html'
    });


