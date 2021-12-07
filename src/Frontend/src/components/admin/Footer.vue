<template lang="pug">
footer.footer
  .container-fluid
    .row.text-muted
      .col-6.text-start
        p.mb-0
          a.text-muted(href='https://ajansnaber.com' target='_blank')
            strong AjansNaber? Yaratıcı Çözümler
          |  ©
      .col-6.text-end
        ul.list-inline
          li.list-inline-item
            a.text-muted(href='https://ajansnaber.com/' target='_blank') Support
          li.list-inline-item
            a.text-muted(href='https://ajansnaber.com/' target='_blank') Help Center
          li.list-inline-item
            a.text-muted(href='https://ajansnaber.com/' target='_blank') Privacy
          li.list-inline-item
            a.text-muted(href='https://ajansnaber.com/' target='_blank') Terms
</template>
<script>
export default {
  name: "Footer"
}
</script>
