<template>
  <div id="app">
    <Navigation />
    <span>Home</span>
  </div>
</template>

<script>
import Navigation from "@/components/Navigation.vue";

export default {
  components: {
    Navigation,
  },

  data: () => ({
    //
  }),
};
</script>

<style lang="scss">
@import "@/styles.scss";
</style>
