<script>
import { mapActions } from "vuex"
import Auth from "@/models/auth"
import Loader from "@/components/shared/Loader.vue"

export default {
  name: "login",
  components: {
    Loader
  },
  data() {
    return {
      user: new Auth(),
      loading: false,
      backendError: {
        error: false,
        message: null
      }
    }
  },
  methods: {
    ...mapActions("account", ["login"]),
    submitLogin(e) {
      e.preventDefault()

      this.loading = true
      this.$validator.validateAll().then(async (isValid) => {
        if (!isValid) {
          this.loading = false
          return
        }

        try {
          const response = await this.login(this.user)

          if (!response.error) {
            this.$router.push("/admin")
          } else {
            this.backendError = {
              error: true,
              message: response.message
            }
          }
        } catch (e) {
          this.backendError = {
            error: true,
            message: e.message
          }
        } finally {
          this.loading = false
        }
      })
    }
  }
}
</script>

<template lang="pug">
.col-sm-10.col-md-8.col-lg-6.mx-auto.d-table
  .d-table-cell.align-middle
    .card
      .card-body
        h1.h2.text-center {{ $t("account.login.title") }}
        .m-sm-4
          .alert.alert-warning(v-if="backendError.error"
            role="alert")
            |  {{ backendError.message }}
          loader(v-if="loading")
          form(name="loginForm"
            v-else
            @submit.prevent="submitLogin")
            .mb-3
              label.form-label {{ $t("account.login.email") }}
              input.form-control.form-control-lg(type="email" 
                name="email"
                autocomplete="email"
                :class="errors.has('email') ? 'is-invalid' : 'is-valid'"
                :disabled="loading"
                :placeholder='$t("account.login.email_placeholder")'
                v-model="user.email"
                v-validate="'required|email'")
              .valid-feedback(v-if="errors.has('email')") {{ errors.email }}
            .mb-3
              label.form-label {{ $t("account.login.password") }}
              input.form-control.form-control-lg(type="password" 
                name="password" 
                autocomplete="password"
                :class="errors.has('password') ? 'is-invalid' : 'is-valid'"
                :disabled="loading"
                :placeholder='$t("account.login.password_placeholder")'
                v-model="user.password"
                v-validate="'required|min:8'")
              .valid-feedback(v-if="errors.has('password')") {{ errors.password }}
              small
                router-link(to="/account/forgot-password") {{ $t("account.login.forgot_password") }}
            .text-center.mt-3
              button.btn.btn-lg.btn-primary(type="submit") {{ $t("account.login.submit") }}
</template>

<style lang="postcss" scoped></style>
