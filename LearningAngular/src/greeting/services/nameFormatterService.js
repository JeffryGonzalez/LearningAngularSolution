(function () {
    angular.module("app")
        .factory("formatterService", function () {
            // header
            var service = {
                formatName: formatName
            };
            return service;
            // implementation
            function formatName(first, last) {
                return last + ", " + first;
            };

        });
})(); // IIFE