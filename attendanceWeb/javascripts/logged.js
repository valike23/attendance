/// <reference path="angular.js" />
/// <reference path="angular-ui-router.js" />
(function () {
    var app = angular.module('app', ['ui.router']);
    app.config(function ($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/dash",
            templateUrl: "./templates/logged/dashboard.htm",
            controller: "homeCtrl"
        }).state('addstudent', {
            url: "/addstudent",
            templateUrl: "./templates/logged/addstudent.htm",
            controller: "studentCtrl"
            }).state('lecturer', {
                url: "/manageLecturers",
                templateUrl: "./templates/logged/lecturer.htm",
                controller: "lecturerCtrl"
            }).state('signup', {
            url: "/signup",
            templateUrl: "/signup.htm",
            controller: "homeCtrl"
            }).state('app', {
                url: "/app_settings",
                templateUrl: "./templates/logged/app.htm",
                controller: "courseCtrl",
                abstract: true
            }).state('app.courses', {
                url: "/courses",
                templateUrl: "./templates/logged/course.htm",
                controller: "courseCtrl"
            }).state('app.department', {
                url: "/department",
                templateUrl: "./templates/logged/department.htm",
                controller: "courseCtrl"
            }).state('class', {
                url: "/class",
                templateUrl: "./templates/logged/class.htm",
                controller: "classCtrl"
            }).state('attendance', {
                url: "/attendance",
                templateUrl: "./templates/logged/attendance.htm",
                controller: "attendCtrl"
            })
            .state('intelligence', {
                url: "/intelligence",
                templateUrl: "./templates/logged/intelligence.htm",
                controller: "intelligenceCtrl"
            })
        $urlRouterProvider.otherwise('/dash');
    });
    app.directive('lecturerTable', function ($http, $state) {
        function link(scope, element, attrs) {
        
            scope.$watch('lecturers', function (New, Old) {
         
            });
            scope.addLecturer = function (lecturer) {
                console.log(lecturer);
                let course = JSON.parse(sessionStorage.getItem('course'));
                let data = {
                    lecturer: lecturer,
                    courseId: course.id
                }
                $http.post('./api/registerLec', data).then(function (res) {
                 
                    alert(`${lecturer.firstName + ' ' + lecturer.lastName} has been added to ${course.code}`);
                    $('#addLecturer').modal('hide');
                    $state.reload();
                })

                for (let i = 0; i < scope.lecturers.length; i++) {
                    if (scope.lecturers[i].id == lecturer.id) {
                        scope.lecturers[i] = lecturer;
                    }
                }
            }

        }
        return {
            link: link,
            restrict: "E",
            scope: {
                lecturers:"="
            },
            templateUrl: "./templates/directives/lecturers.htm"
        };
    })

    app.controller('homeCtrl', function ($scope, $http) {
        $http.get("./api/dashboard").then(function (res) {
            $scope.dashboard = res.data;
        
        })

        var randomScalingFactor = function () {
            return Math.round(Math.random() * 100);
        };

        var config2 = {
            type: 'pie',
            data: {
                datasets: [{
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                    ],
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.orange,
                        window.chartColors.yellow,
                        window.chartColors.green,
                        window.chartColors.blue,
                    ],
                    label: 'Dataset 1'
                }],
                labels: [
                    'Red',
                    'Orange',
                    'Yellow',
                    'Green',
                    'Blue'
                ]
            },
            options: {
                responsive: true
            }
        };
        var ctx2 = document.getElementById('chart-area').getContext('2d');
        window.myPie = new Chart(ctx2, config2);
        var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        var config = {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
                datasets: [
                    {
                    label: 'CSC110',
                    backgroundColor: window.chartColors.red,
                    borderColor: window.chartColors.red,
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor()
                    ],
                    fill: false,
                    },
                    {
                    label: 'CSC123',
                    fill: false,
                    backgroundColor: window.chartColors.blue,
                    borderColor: window.chartColors.blue,
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor()
                    ],
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Chart.js Line Chart'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Month'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Value'
                        }
                    }]
                }
            }
        };
        var ctx = document.getElementById('canvas').getContext('2d');
        window.myLine = new Chart(ctx, config);
       
    });
    app.controller('navCtrl', function ($scope, $rootScope) {
        $scope.logout = function () {
          
            $rootScope.user = null;
            sessionStorage.removeItem("user");
            location.href = './loggedout.html#/home';
        }
    })
    app.controller('intelligenceCtrl', function ($scope, $rootScope, $http) {
        $scope.lecturer =JSON.parse( sessionStorage.getItem("user"));
        $scope.courses = $scope.lecturer.department.courses;
        $scope.getCourse = function (id) {
            $http.get('./api/courses/'+ id).then(function (res) {
                $scope.course = res.data;
                $scope.students = $scope.course.students;
              
            }, function (err) {
                alert("Details failed");
                console.log(err);
                })
        }

    })

    app.controller('courseCtrl', function ($scope, $rootScope, $http, $state) {
        
        $scope.savedept = function () {
          
            $http.post('./api/departments',$scope.data).then(function (res) {
                $scope.user = null;
                alert("Department Added Successfully");
                $('#deptModal').modal('hide');
                $state.reload();
            })
           
        }
        $scope.saveCourse = function () {
     
            $http.post('./api/courses', $scope.data).then(function (res) {
                $scope.user = null;
                alert("Course Added Successfully");
                $('#courseModal').modal('hide');
                $state.reload();
            })
        }
        $scope.nav = function (nav) {
            let course = document.getElementById("course");
            let department = document.getElementById("department");
            course.classList.remove("d-active");
            department.classList.remove("d-active");
            if (nav == 'course') {
                $state.go('app.courses');
                course.classList.add('d-active');
            }
            if (nav == 'department') {
                $state.go('app.department');
                department.classList.add('d-active');
            }
            
            
        }

        $scope.openlec = function (course) {
            console.log(course);
            sessionStorage.setItem('course', JSON.stringify(course));
            $http.get("./api/lecturers").then(function (res) {
                $rootScope.lecturers = res.data;
                console.log($rootScope.lecturers);
                for (let i = 0; i < $rootScope.lecturers.length; i++) {
                    $rootScope.lecturers[i].checked = false;
                    for (let j = 0; j < course.lecturers.length; j++) {
                        if (course.lecturers[j].id == $rootScope.lecturers[i].id) {
                            $rootScope.lecturers[i].checked = true;
                        }
                    } 
                }
               
                $('#addLecturer').modal('show');
           
            })
        }
        $http.get("./api/departments").then(function (res) {
            $scope.departments = res.data;
          
        });
       
        $http.get("./api/courses").then(function (res) {
            $scope.courses = res.data;
          //  console.log($scope.courses);
        
        })
    })
    app.controller('studentCtrl', function ($scope, $http, $state) {
        $scope.fingersCount = [];
        $scope.selectedCourses = [];
        $scope.student = {};
        $scope.switch = function (data) {
            $scope.showHand = data;
        }
        $scope.showHand = false;
        var inter;
        $scope.open = function () {

        }
        $scope.done = true;
        $scope.submitCourses = function () {
            var courses = [];
            for (var i = 0; i < $scope.courses.length; i++) {
                if ($scope.courses[i].isChecked) {
                    delete ($scope.courses[i].isChecked);
                    courses.push($scope.courses[i]);
                }
            }
            $scope.courses = [];
            var student = JSON.parse(sessionStorage.getItem("student"));
            var id = student.id;
            delete (student.id);
            student.courses = courses;
            student.courseReg = 1;
            console.log(student);
            $http.put("./api/students/" + id, student).then(function (res) {
               alert("course Reg susscessful")
            });

        }
        $('#courseReg').on('shown.bs.modal', function () {
            $http.get("./api/courses").then(function (res) {
                $scope.courses = res.data;
            });
        });
        $scope.checked = function (event, course) {
            event.preventDefault(true);
           
            
            console.log(event);
            for (var i = 0; i < $scope.courses.length; i++) {
                if ($scope.courses[i] === course) {
                    if ($scope.courses[i].isChecked) {
                        event.currentTarget.setAttribute("class", "checkme  ");
                        $scope.courses[i].isChecked = false;
                    } else {
                        event.currentTarget.setAttribute("class", "checkme fa fa-check ");
                        $scope.courses[i].isChecked = false;
                    }
                }
            }
        }
        $scope.openCourse = function (student) {
            sessionStorage.setItem("student", JSON.stringify(student));
        };

        $http.get("./api/students").then(function (res) {
            $scope.students = res.data;
            console.log($scope.students);
        });

        $scope.save = function () {
            function mytemp(str) {
                let temp = str.substring(3);
                return isNaN(temp);
            }
            $scope.user.deptId = $scope.dept.id;
            let str = $scope.user.matNo;
            let temp = str.substring(0, 3);

          temp =  temp.toLowerCase();
            if (temp != 'psc' || mytemp($scope.user.matNo)) {
                alert("The Matriculation Number is Invalid");
                return;
            }

           
            

           
            $http.post("./api/students", $scope.user).then(function (res) {
                
                $scope.students = res.data;

                inter = setInterval(function () {
                    $http.get('./api/webInstrucs').then(
                        function (res) {
                            $scope.hideHand = false;
          
                            var data = res.data;
                            if (data.name != null) {
                                $scope.web = res.data;
                                if (data.name == "Registration Completed Successfully!!!") {
                                    alert("Capture Completed");
                                    $state.go("addstudent");
                                }
                            }

                        },
                        function (err) {
                            console.log(err);
                        })
                }, 1500)
            })
        }
        $http.get("./api/departments").then(function (res) {
            $scope.departments = res.data;
         
        })
        $('#myModal').on('shown.bs.modal', function (e) {
         
        })
        $('#myModal').on('hidden.bs.modal', function (e) {
            clearInterval(inter);
        })
       
    })
    app.controller('classCtrl', function ($scope, $http, $rootScope, $state) {
        $scope.startClass = function (myclass) {
         
            var confirmed = window.confirm("Are you sure you want to begin this class?")
            if (confirmed) {
             
                alert("class has started");
                $http.get("./api/myclasses/" + myclass.Id).then(function (res) {
                    $rootScope.myClass = myclass;
                    $state.go("attendance");
                }, function (err) {
                    alert("class could not start.. Please contact Admin");
                })
               
            }
        }
        $http.get('./api/courses').then(
            function (res) {
        
                $scope.courses = res.data;
            },
            function (err) {

            })
        $http.get('./api/classes').then(
            function (res) {

                $scope.classes = res.data;
            },
            function (err) {

            })

        $scope.subClass = function () {
            $('#addClass').modal('hide')
            $('#loader').modal('show')
            var data = {
                course: $scope.selectedcourse.id,
                date: $scope.date,
                period: $scope.period
            }
     
            $http.post("./api/classes", data).then(function (res) {
                $('#loader').modal('hide');

                $state.reload();

            }, function (err) {
                $('#loader').modal('hide');
                alert("class not created")
            })
        }
    });
    app.controller('attendCtrl', function ($scope, $rootScope, $http,$state) {
        var numb = 0;
        setInterval(
            function () {
                $http.get("./api/verfications").then(function (res) {
                    if (res.data.name == null) return;
                
                 numb = numb + 1;
           
                    var str = numb + ". " + res.data.name + "\n";
                    if (res.data.name == 'Verified') {
                        alert("you have been marked");
                    }
                    if (res.data.name == 'Finger print not recognised') {
                        alert("This finger was not recognized")
                    }
            if ($scope.instruction != undefined) {
                $scope.instruction = str.concat($scope.instruction);
            }
            else {
                $scope.instruction = str;
            }
            })
        }, 2000)
        $scope.stopClass = function () {
           
            $http.post("./api/myclasses/" + $rootScope.myClass.Id).then(function (res) {
         
                alert("Class has Ended");
                $state.go("home");
            }, function (err) {
                console.log(err)
                alert("Class couldnt close, Note Course will close after time is reached!. Contact Admin.")
            })
        }
      
        $scope.confirmMat = function () {
           
           
        }
    })
    app.controller('lecturerCtrl', function ($scope, $rootScope, $http) {
        $scope.save = function () {
            $http.post('./api/lecturers', $scope.user).then(function (res) {
                alert("lecturer  Added ")
            }, function (err) {
                console.log(err)
                alert("something went wrong")})
        }
        $http.get("./api/departments").then(function (res) {
            $scope.departments = res.data;
           
        })
        $http.get("./api/lecturers").then(function (res) {
            $scope.lecturers = res.data;
      
        })
    })
    app.run(function () {
        let user = sessionStorage.getItem("user");

        if (!user) {
            location.href = './';
        }
    })
    function toogleLoader() {
        $('#loader').modal('toggle');
    }

})()