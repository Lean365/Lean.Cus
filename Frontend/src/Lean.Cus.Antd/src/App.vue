<template>
  <a-config-provider>
    <ChatRoom />
  </a-config-provider>
</template>

<script setup lang="ts">
import ChatRoom from './components/ChatRoom.vue';
</script>

<style>
#app {
  height: 100vh;
  margin: 0;
  padding: 0;
}
</style>
