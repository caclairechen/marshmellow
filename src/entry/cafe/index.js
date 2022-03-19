import Vue from "vue";
import page from "@/pages/cafe.vue";
import vuetify from "@/plugins/vuetify";

Vue.config.productionTip = false;

new Vue({
  vuetify,
  render: (h) => h(page),
}).$mount("#app");
