import Vue from "vue"
import App from "./App.vue"
import router from "./routes"
import store from "./store"
import "./registerServiceWorker"

import VeeValidate from "vee-validate"

import i18n from "./locales/i18n"

Vue.config.productionTip = false

Vue.use(VeeValidate)

async function main() {
  let storeInstance = await store()

  new Vue({
    router: router(storeInstance),
    store: storeInstance,
    i18n,
    render: (h) => h(App)
  }).$mount("#app")
}

main()
