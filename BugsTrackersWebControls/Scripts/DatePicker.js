function CreateDatePicker(imageUrl) {
    $(function () {

        $(function () { $("input[type='text']:enabled:first").focus(); });

        $("#DatePickerControl_dateTextBox").datepicker({
            showOn: "button",
            buttonImage: imageUrl,
            buttonImageOnly: true
        });


       // $("#DatePickerControl_dateTextBox").datepicker("option", "showAnim", 'fold');
        $("#DatePickerControl_dateTextBox").datepicker($.datepicker.regional["ro"]);

    });



}




