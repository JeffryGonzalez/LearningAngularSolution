(function() {
    angular.module("app.students")
        .config([
            '$urlRouterProvider',
            '$stateProvider',
            function($urlRouterProvider, $stateProvider) {
                $stateProvider.state("default.students", {
                    url: "students",
                    views: {
                        "content@": {
                            templateUrl: "/src/mentoring/students/content.html"
                        },
                        "header@": {
                            template: "<h1>Students Information</h1>"
                            
                        }
                    }
                });
            }
        ]);
})();