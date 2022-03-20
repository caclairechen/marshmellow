import Vue from "vue";
import page from "@/pages/pets.vue";
import vuetify from "@/plugins/vuetify";
import store from "@/store";
import axios from "axios";

Vue.prototype.$http = axios;
Vue.config.productionTip = false;

new Vue({
  vuetify,
  store,
  render: (h) => h(page),
}).$mount("#app");
