$(function () {
    $("#MainCategories").click(function () {
        var value;

        $("#MainCategories :selected").each(function (i, selected) {
            value = selected.text;
        });

        //debugger;

        var postData = { categoryName: value };
        $.ajax({
            type: "POST",
            url: '/auction/getcategories',
            data: postData,
            success: function (data) {
                $("#SubCategories").children().remove();
                for (var i = 0; i < data.length; i++) {
                    $("#SubCategories").append("<option value='" + data[i] + "'>" + data[i] + "</option>");
                }
            },
            traditional: true
        });
    });
})