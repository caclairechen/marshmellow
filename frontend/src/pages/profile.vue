<template>
  <v-app>
    <div data-app id="app">
      <Navigation />
      <span>My Profile</span>
      <div v-for="user in users" v-bind:key="user.id">
        <h2>{{ user.name }}</h2>
      </div>
    </div>
  </v-app>
</template>

<script>
import Navigation from "@/components/Navigation.vue";

export default {
  components: {
    Navigation,
  },
  computed: {
    users() {
      return this.$store.state.users;
    },
  },
  created() {
    this.$store.dispatch("loadUsers");
  },
};
</script>
