"use strict"

document.addEventListener('DOMContentLoaded', function () {
   const form = document.getElementById('edit__form_second'); 

    const formImage = document.getElementById('formImage');
  
    const formPreview = document.getElementById('formPreview');


    formImage.addEventListener('change', () => {
        uploadFile(formImage.files[0]);
    });

    function uploadFile(file) {
        if (!['image/jpeg', 'image/png', 'image/gif'].includes(file.type)) {
            alert('Разрешены только изображения.');
            formImage.value = '';
            return;
        }

        if (file.size > 2 * 1024 * 1024) {
            alert('Файл должен быть менее 2 мб.');
            return;
        }

        var reader = new FileReader();
        reader.onload = function (e) {
            formPreview.innerHTML = `<img src="${e.target.result}" alt="Фото">`;
        };
        reader.onerror = function (e) {
            alert('Ошибка');
        };
        reader.readAsDataURL(file);
    }
});

var picker = new Lightpick({
    field: document.getElementById('calendar'),
    singleDate: false,
    inline: true,
    lang: 'ru',
    onSelect: function(start, end){
        var str = '';
        str += start ? start.format('Do MMMM YYYY') + ' to ' : '';
        str += end ? end.format('Do MMMM YYYY') : '...';
        document.getElementById('result').innerHTML = str;
    }
});