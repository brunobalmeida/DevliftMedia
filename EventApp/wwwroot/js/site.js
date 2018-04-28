// Write your JavaScript code.
jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        var isChrome = window.chrome;
        // make correction for chrome
        if (isChrome) {
            var d = new Date();
            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        }
        // leave default behavior
        else {
            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(value));
        }
    }
});



