<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <div class="wrapper">
      <h3>Помощник кладовщика</h3>
      <nav class="navbar navbar-expand-sm bg-light navbar-dark">
        <ul class="navbar-nav">
          <li class="nav-item m-1">
              <RouterLink class="btn btn-light btn-outline-primary" to="/transfer">Список всех перемещений</RouterLink>
          </li>
        </ul>
        <ul class="navbar-nav">
          <li class="nav-item m-1">
              <RouterLink class="btn btn-light btn-outline-primary" to="/storage">Список перемещений по конкретному складу</RouterLink>
          </li>
        </ul>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

