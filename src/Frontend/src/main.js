import Vue from "vue"
import App from "./App.vue"
import router from "./routes"
import store from "./store"
import VueMeta from "vue-meta"
import "./registerServiceWorker"

import notifications from "vue-notification"
import notifyMixin from "@/mixins/notify"
import pagination from "./mixins/pagination"

Vue.config.productionTip = false

Vue.use(notifications)
Vue.prototype.$backendUrl = process.env.VUE_APP_BACKEND_URL
Vue.mixin(notifyMixin)
Vue.mixin(pagination)

Vue.use(VueMeta, {
  refreshOnceOnNavigation: true
})

new Vue({
  router,
  store,
  render: (h) => h(App)
}).$mount("#app")
