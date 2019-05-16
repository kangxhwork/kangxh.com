//<div id="screenshot" style="text-align:center;">
//    <video class="videostream" autoplay></video>
//    <img id="screenshot-img">
//
// <button class="capture-button">Start Camera</button>
// <button id="screenshot-button" disabled>Take Screenshot</button>
// <button id="saveBlob-button" disabled>Save Image to Blob</button>
// </div>

const constraints = { video: true };

(function ()
{
    const captureVideoButton = document.querySelector('#screenshot .capture-button');
    const saveBlobButton = document.querySelector('#saveBlob-button');
    const screenshotButton = document.querySelector('#screenshot-button');

    const img = document.querySelector('#screenshot img');
    const video = document.querySelector('#screenshot video');

    const canvas = document.createElement('canvas');

    captureVideoButton.onclick = function () {
        navigator.mediaDevices.getUserMedia(constraints).
            then(handleSuccess).catch(handleError);
    };

    screenshotButton.onclick = video.onclick = function () {
        saveBlobButton.disabled = false;

        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        canvas.getContext('2d').drawImage(video, 0, 0);
        img.src = canvas.toDataURL('image/webp');
    };

    saveBlobButton.onclick = function () {
        blobInfoForm = document.forms['blobInfoForm'];

        var sasKey = blobInfoForm.elements["sasTokenTextBox"].value; 
        var blobUri = blobInfoForm.elements["blobAccountTextbox"].value; 
        var blobService = AzureStorage.Blob.createBlobServiceWithSas(blobUri, sasKey);
        
        //var imageStream = canvas.toDataURL("image/png").replace("image/png", "image/octet-stream"); 
    

        blobService.createBlockBlobFromText('webcam', 'myblob.png', img.src, function (error, result, response) {
        //blobService.createBlockBlobFromStream('webcam', 'myblob', imageStream, imageStream.length, function (error, result, response) {
                if (error) {
                    alert('Upload filed, open browser console for more detailed info.');
                    console.log(error);
                } else {
                    alert('Upload successfully!');
                }
            });
        }

    function handleSuccess(stream) {
        screenshotButton.disabled = false;
        video.srcObject = stream;
    };

    function handleError(error) {
        console.error('navigator.getUserMedia error: ', error);
    }
})();