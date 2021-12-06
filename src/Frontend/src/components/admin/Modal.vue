<template lang="pug">
.modal.fade(tabindex="-1" aria-hidden="true")
  .modal-dialog
    .modal-content
      .modal-header
        slot(name="modal-title")
          h5.modal-title Modal title
        button.btn-close(type="button" data-bs-dismiss="modal" aria-label="Close")
      .modal-body
        slot(name="modal-body") default body
      .modal-footer
        slot(name="footer")
          button.btn.btn-secondary(type="button" data-bs-dismiss="modal") Close
          button.btn.btn-primary(type="button" data-bs-dismiss="modal" @click="$emit("ok")") Ok
</template>
<script>
export default {
  name: "Modal"
}
</script>
