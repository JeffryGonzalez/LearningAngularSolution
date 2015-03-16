(function () {
    angular.module("app", [])
        .controller("NameFormatter", [
            'formatterService',
            function (formatterService) {
            var vm = this;
            vm.greeting = "Is this thing on?";
            vm.greetings = [];
            vm.sendGreeting = function () {
                var greeting = formatterService.formatName(vm.firstName, vm.lastName);
                alert(greeting);
                vm.greetings.push(greeting);
                vm.firstName = "";
                vm.lastName = "";
            };
        }]);
})();