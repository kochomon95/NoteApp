import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import NoteList from './views/Note/NoteList.vue'

const app = createApp(NoteList)

app.use(router)

app.mount('#app')
