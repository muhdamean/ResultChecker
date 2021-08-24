/* cascading dropdown */
$(document).ready(function () {
    var department = $("#Department");
    var doctor = $("#Doctor");
    doctor.prop('disabled', true);

    department.change(function () {
        if ($(this).val() == "") {
            doctor.append($('<option>', { value: '', text: '---- Select Doctor ----' }, '</option>'));
            doctor.prop('disabled', true);
            doctor.val("");
        }
        else {
            $.ajax({
                url: "https://localhost:44376/home/GetDoctor",
                method: "GET",
                data: { department: $(this).val() },
                success: function (data) {
                    /* console.log(data);*/
                    doctor.prop('disabled', false);
                    doctor.empty();
                    doctor.append($('<option>', { value: '0', text: '---- Select Doctor ----' }, '</option>'));
                    $(data).each(function (index, item) {
                        doctor.append($('<option>', { value: item.value, text: item.text }, '</option>'))
                    });
                    /* console.log("finish");*/
                }
            });
        }
    });
});