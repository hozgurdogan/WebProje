
// Textboxların içinde phonenumber class kullananları bularak yanlızca email adresi girilmesini sağlar.
Inputmask({
    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
    greedy: false,
    onBeforePaste: function (pastedValue, opts) {
        pastedValue = pastedValue.toLowerCase();
        return pastedValue.replace("mailto:", "");
    },
    definitions: {
        "*": {
            validator: '[0-9A-Za-z!#$%&"*+/=?^_`{|}~\-]',
            cardinality: 1,
            casing: "lower"
        }
    }
}).mask(".emailaddress");

function toPhoneNumber(phoneNumber) {
    var cleaned = ('' + phoneNumber).replace(/\D/g, '');
    var match = cleaned.match(/^(\d{1})(\d{3})(\d{3})(\d{4})$/);
    if (match) {
        return match[1] + " (" + match[2] + ") " + match[3] + " " + match[4];
    }
    return null;
}

function toDateTime(date) {
    try {
        if (!date)
            return "-";
        var dateTime = date;
        if (Array.isArray(date))
        dateTime = date[0]
        if (!dateTime)
            return "-";
        if (dateTime.includes("."))
            dateTime = dateTime.split(".")[0]

        var date = dateTime.split("T")[0];
        var time = dateTime.split("T")[1];

        var dateSeparated = date.split("-");
        var timeSeparated = time.split(":");

        var newDateTime = `${dateSeparated[2]}.${dateSeparated[1]}.${dateSeparated[0]} ${timeSeparated[0]}:${timeSeparated[1]}`;

        return newDateTime;
    }
    catch {
        return "-"
    }
}

// DatePicker için kullanılır.

var start = moment().subtract(29, "days");
var end = moment();

function cb(start, end) {
    $(".daterangepicker").html(start.format("MMMM D, YYYY") + " - " + end.format("MMMM D, YYYY"));
}
function splitLongDescription(item) {
    return item.length > 50 ? item.substring(0, 50) + ".." : item
}

$(".daterangepicker").daterangepicker({
    startDate: start,
    endDate: end,
    ranges: {
        "Bugün": [moment(), moment()],
        "Dün": [moment().subtract(1, "days"), moment().subtract(1, "days")],
        "Son 7 Gün": [moment().subtract(6, "days"), moment()],
        "Son 30 Gün": [moment().subtract(29, "days"), moment()],
        "Bu Ay": [moment().startOf("month"), moment().endOf("month")],
        "Önceki Ay": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")]
    },
    "locale": {
        "format": "DD.MM.YYYY",
        "separator": " - ",
        "applyLabel": "Uygula",
        "cancelLabel": "Vazgeç",
        "fromLabel": "From",
        "toLabel": "To",
        "customRangeLabel": "Özel",
        "daysOfWeek": [
            "Paz",
            "Pzt",
            "Sal",
            "Çar",
            "Per",
            "Cum",
            "Cts"
        ],
        "monthNames": [
            "Ocak",
            "Şubat",
            "Mart",
            "Nisan",
            "Mayıs",
            "Haziran",
            "Temmuz",
            "Ağustos",
            "Eylül",
            "Ekim",
            "Kasım",
            "Aralık"
        ],
        "firstDay": 1
    }
}, cb);

cb(start, end);

$(".datetimepicker").flatpickr({
    "enableTime": true,
    defaultDate: null,
    "dateFormat": "d.m.y H:i",
    "locale": "tr"
});

$(".datetimepickerwithoutTime").flatpickr({
    "enableTime": false,
    defaultDate: null,
    "dateFormat": "d.m.y",
    "locale": "tr"
});

var datePicker;
$(".datepicker").flatpickr({
    enableTime: true,
    dateFormat: "d.m.Y H:i",
    locale: "tr",
    defaultDate: null,
    time_24hr: true,
    onReady: function () {
        datePicker = this;
    }
});

var deleteText = {
    title: 'Silmek istediğinize emin misiniz?',
    message: 'Bu işlem geri döndürülemeyecek şekilde verileri silecektir.'
}
var statusText = {
    title: 'Kullanıcının Aktifliğini Değiştir',
    message: 'Kullanıcının Aktifliğini değiştirmek istediğinize emin misiniz?'
}

function DeleteEntity(targetUrl, id, info = deleteText) {
    Swal.fire({
        title: info.title,
        text: info.message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sil',
        cancelButtonText: "İptal"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: targetUrl + id,
                success: function (response) {
                    if (response.isSuccess) {
                        ReloadAjax();
                        ShowSuccess(response.message)
                    }
                    else {
                        ShowError(response.message);
                    }
                },
                error: function (err) {
                    ShowError(err.message);
                }
            });
        }
    });
}


function SendPushNotification(name, id) {
    Swal.fire({
        title: name + ' Bildirim Gönder',
        width: '800px',
        html:
            '<div style="width:98%"><div class="row px-1"> <input id="swal-input1" class="form-control form-control-lg form-control-solid"  placeholder="Başlık Giriniz" /></div>' +
            '<div class="row py-6 px-1"><textarea id="swal-input2" class="form-control form-control-lg form-control-solid " cols="30"  placeholder="Mesajınızı Giriniz"></textarea></div></div>',
        preConfirm: function () {
            return new Promise(function (resolve) {
                // Validate input
                if ($('#swal-input1').val() == '' || $('#swal-input2').val() == '') {
                    swal.showValidationMessage("Başlık ve Mesaj kısımlarını doldurunuz"); // Show error when validation fails.
                    swal.enableConfirmButton(); // Enable the confirm button again.
                } else {
                    swal.resetValidationMessage(); // Reset the validation message.
                    resolve([
                        $('#swal-input1').val(),
                        $('#swal-input2').val()
                    ]);
                }
            })
        },
        onOpen: function () {
            $('#swal-input1').focus()
        },
        inputPlaceholder: 'Mesajınız',
        showCancelButton: true,
        confirmButtonText: 'Gönder',
        cancelButtonText: 'İptal',
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Customers/SendNotificaiton",
                type: "POST",
                data: {
                    id: id, title: result.value[0], message: result.value[1]
                },
                success: function (data) {
                    console.log(data)
                    if (data.isSuccess) {
                        Swal.fire({
                            icon: 'success',
                            html: data.message
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            html: 'Statü güncellenemedi.'
                        });
                    }
                }
                });

            }
        });
}

Inputmask({
    "mask": "0 (999) 999 99 99",
    //"placeholder": "+90 (532) 123 45 67",
}).mask(".phonenumber");




function showButtonSubMenu(event) {
    let el = event.nextElementSibling;
    if (el.classList.contains("show"))
        el.classList.remove("show")
    else
        el.classList.add("show")
    document.addEventListener('click', function (event2) {
        var isClickInside = event.parentElement.contains(event2.target);
        if (!isClickInside) {
            event.nextElementSibling.classList.remove("show")
        }
    });
}


function ShowSuccess(message) {
    Swal.fire({
        icon: 'error',
        title: 'İşlem Başarılı',
        text: message,
    })
}

function ShowError(message) {
    Swal.fire({
        icon: 'error',
        title: 'İşlem Başarısız',
        text: message,
    })
}

function ShowWarning(message) {
    Swal.fire({
        icon: 'warning',
        title: 'Dikkat',
        text: message,
    })
}

function SetEditor(targetIdName, url) {
    tinymce.remove('textarea#' + targetIdName)
    var demoBaseConfig = {
        selector: 'textarea#' + targetIdName,
        images_upload_url: url,
        images_file_types: 'jpg,png,jfif',
        width: '100%',
        height: 500,
        resize: false,
        autosave_ask_before_unload: false,
        powerpaste_allow_local_images: true,
        plugins: [
            '  advlist anchor autolink codesample fullscreen help image imagetools ',
            ' lists link media noneditable  preview',
            ' searchreplace table template  visualblocks wordcount'
        ],
        toolbar:
            'insertfile a11ycheck undo redo | bold italic | forecolor backcolor | template codesample | alignleft aligncenter alignright alignjustify | bullist numlist | link image ',
        spellchecker_dialog: true,
        spellchecker_ignore_list: ['Ephox', 'Moxiecode'],
        content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
    };

    tinymce.init(demoBaseConfig);

    return demoBaseConfig;
}