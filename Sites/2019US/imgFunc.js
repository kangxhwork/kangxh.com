//https://www.askingbox.com/tutorial/how-to-resize-image-before-upload-in-browser

var showImage = function (event) {
    var file  = event.target.files[0];
    var reader = new FileReader();

    // read file data
    reader.onload = function (readerEvent) {
        // display the selected file name 
        var name = document.getElementById("photoName");
        name.value = file.name;
        // preview selected image
        var imgPreview = document.getElementById("photoPreview");
        imgPreview.src = reader.result;
    };
    reader.readAsDataURL(file);
};

function sendFile()
{
    // add code here to resize the image if it is larger than 4MB
    var formData = new FormData();

    formData.append('file', $('#photoFile')[0].files[0]);
    formData.append('trip', "2019us");
    var photoLocation = document.getElementById("photoLocation");
    formData.append('location', photoLocation.value);

    $.ajax({
        type: 'post',
        url: '/photo.ashx',
        data: formData,
        success: function (status) {
            console.log(status);
            alert("file uploaded");
        },
        async: false,
        processData: false,
        contentType: false,
        error: function () {
            console.log(status);
            alert("failed to upload");
        }
    });
}
