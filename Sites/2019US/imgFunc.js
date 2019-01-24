
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
};

function sendFile()
{
    var formData = new FormData();
    formData.append('file', $('#photoFile')[0].files[0]);
    formData.append('trip', "2019us");
    $.ajax({
        type: 'post',
        url: '/photo.ashx',
        data: formData,
        success: function (status) {
            console.log(status);
        },
        processData: false,
        contentType: false,
        error: function () {
            console.log(status);
        }
    });
}
