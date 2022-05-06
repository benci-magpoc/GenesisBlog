var quill = new Quill('#quillEditor', {
        theme: 'snow'
});


//Tell the form to do some stuff real quick before it submits the data
document.querySelector("form").addEventListener("submit", function () {
    //Stuff the data currently inside of the Quill editor inside the hidden input the name="Content"
    document.getElementById("Content").value = quill.container.firstChild.innerHTML;   
})


