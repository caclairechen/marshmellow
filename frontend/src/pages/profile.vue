<template>
  <div id="app">
    <Navigation />
    <span>My Profile</span>
    <div>{{ users }}</div>
  </div>
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
<style lang="scss">
@import "@/styles.scss";
</style>
