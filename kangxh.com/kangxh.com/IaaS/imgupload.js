//https://www.askingbox.com/tutorial/how-to-resize-image-before-upload-in-browser

var showImage = function (event) {
    var file  = event.target.files[0];
    var reader = new FileReader();
    // read file data
    reader.onload = function (readerEvent) {
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

    $.ajax({
        type: 'post',
        url: '/iaas/imghandler.ashx',
        data: formData,
        async: false,
        processData: false,
        contentType: false,
        success: function (status) {
            alert("file uploaded");
        },
        error: function () {
            alert("failed to upload");
        }
    });
}
