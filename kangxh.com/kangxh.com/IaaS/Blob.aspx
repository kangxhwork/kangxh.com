<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blob.aspx.cs" Inherits="kangxh.com.html5.IaaS.Blob" %>

<!DOCTYPE html>
<!-- https://www.html5rocks.com/en/tutorials/getusermedia/intro/#toc-screenshot -->


<div id="screenshot" style="text-align:center;">
    <video class="videostream" autoplay></video>
    <img id="screenshot-img">
    <p><button class="capture-button">Capture video</button>
    <p><button id="screenshot-button" disabled>Take screenshot</button></p>
</div>

<script>
    function handleError(error) {
      console.error('navigator.getUserMedia error: ', error);
    }
    const constraints = {video: true};

    (function() {
      const captureVideoButton =
        document.querySelector('#screenshot .capture-button');
      const screenshotButton = document.querySelector('#screenshot-button');
      const img = document.querySelector('#screenshot img');
      const video = document.querySelector('#screenshot video');
    
      const canvas = document.createElement('canvas');
    
      captureVideoButton.onclick = function() {
        navigator.mediaDevices.getUserMedia(constraints).
          then(handleSuccess).catch(handleError);
      };
    
      screenshotButton.onclick = video.onclick = function() {
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        canvas.getContext('2d').drawImage(video, 0, 0);
        // Other browsers will fall back to image/png
        img.src = canvas.toDataURL('image/webp');
      };
    
      function handleSuccess(stream) {
        screenshotButton.disabled = false;
        video.srcObject = stream;
      }
    })();
    
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Save Block blob via SAS</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
