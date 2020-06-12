/// <reference path="angular.js" />
/// <reference path="angular-ui-router.js" />
(function () {
    var app = angular.module('app', ['ui.router']);
    app.config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/home",
            templateUrl: "./templates/loggedout/login.htm",
            controller: "homeCtrl"
        }).state('about', {
            url: "/about",
            templateUrl: "/about.htm",
            controller: "homeCtrl"
        }).state('signup', {
            url: "/signup",
            templateUrl: "/signup.htm",
            controller: "homeCtrl"
            })
        $urlRouterProvider.otherwise('/home');
    })

    app.controller('homeCtrl', function ($scope, $http) {
        $scope.login = function () {
            toogleLoader();
            var loginBtn = document.getElementById("login");
            loginBtn.disabled = true;
            var data = {
                user: $scope.user,
                password: $scope.password
            };
            $http.post('./api/login', data).then(function (res) {
                
                toogleLoader();
                loginBtn.disabled = false;
               
                if (res.data == null) {
                    alert("username or password in correct");
                }
                else {
                    sessionStorage.setItem('user', JSON.stringify(res.data))
                    location.href = './logged.html';
                }
            },
                function (err) {
                alert("server error");
                console.log(err);
                toogleLoader();
                loginBtn.disabled = false;
            })
        }
    })

    function toogleLoader() {
        var loader = document.getElementById("spinner");
        var state = loader.classList.contains('is-active');
        if (state) {
            loader.classList.remove('is-active')
        }
        else {
            loader.classList.add('is-active')
        }
    }

})()