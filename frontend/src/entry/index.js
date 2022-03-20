import Vue from "vue";
import page from "@/pages/index.vue";
import vuetify from "@/plugins/vuetify";
import store from "@/store";

Vue.config.productionTip = false;

new Vue({
  vuetify,
  store,
  render: (h) => h(page),
}).$mount("#app");
