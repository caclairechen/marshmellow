<template>
  <nav>
    <ul>
      <li><a href="/">Home</a></li>
      <li><a href="/pets">My Pets</a></li>
      <li><a href="/cafe">My Cafe</a></li>
      <li><a href="/profile">My Profile</a></li>
    </ul>
  </nav>
</template>

<style scoped>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  display: flex;
  justify-content: center;
}

li:not(:last-child) {
  margin-right: 16px;
}
</style>
