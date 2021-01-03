<template>
  <div>
    <h2 class="home">Witamy w YouKPI</h2>
    <ProdExe />
  </div>
</template>

<script>
// @ is an alias to /src
import ProdExe from '../components/ProdExe.vue'

export default {
  name: 'Home',
  components: {
    ProdExe
  }
}
</script>
