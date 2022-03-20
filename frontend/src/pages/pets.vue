<template>
  <div id="app">
    <Navigation />
    <span>My Pets</span>
  </div>
</template>

<script>
import Navigation from "@/components/Navigation.vue";

export default {
  components: {
    Navigation,
  },
};
</script>
<style lang="scss">
@import "@/styles.scss";
</style>
