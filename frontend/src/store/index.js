import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    test: 1,
  },
  getters: {},
  mutations: {
    setTest(state, val) {
      state.test = Number(val);
    },
  },
  actions: {},
});
