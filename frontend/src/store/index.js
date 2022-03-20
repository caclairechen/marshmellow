import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    users: [],
  },
  getters: {},
  mutations: {
    SET_USERS(state, val) {
      state.users = val;
    },
  },
  actions: {
    async loadUsers({ commit }) {
      try {
        const response = await this.$http.get("https://localhost:7124/Users");
        commit("SET_USERS", response.data);
      } catch (error) {
        console.log(error);
      }
    },
  },
});
