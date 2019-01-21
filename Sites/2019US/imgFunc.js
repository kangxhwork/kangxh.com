
var showImage = function (event) {
    var input = event.target;
    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        var preview = document.getElementById("photoPreview");
        var name = document.getElementById("photoName");
        preview.src = dataURL;
        name.value = input.files[0].name;
    };
    reader.readAsDataURL(input.files[0]);
};function sendFile() {

    var formData = new FormData();
    formData.append('file', $('#photoFile')[0].files[0]);
    fromData.append('trip', '2019us');
    formData.append('location', $('#photoName'));
    $.ajax({
        type: 'post',
        url: 'fileUploader.ashx',
        data: formData,
        success: function (status) {
            //if (status != 'error') {
            //    var my_path = "MediaUploader/" + status;
            //    $("#photoFile").attr("src", my_path);
            //}
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}