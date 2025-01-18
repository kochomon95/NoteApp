<template>
    <div class="container mx-auto p-4">
      <h1 class="text-xl mb-4">Notes</h1>
  
      <!-- Create Note Form -->
      <form @submit.prevent="createNote" class="mb-4">
        <input v-model="newNote.title" type="text" placeholder="Title" class="border p-2 mb-2 w-full" required />
        <textarea v-model="newNote.content" placeholder="Content" class="border p-2 mb-2 w-full" required></textarea>
        <button type="submit" class="bg-blue-500 text-white p-2 rounded">Create Note</button>
      </form>
  
      <!-- Notes List -->
      <div v-if="notes.length > 0" class="space-y-4">
        <div v-for="note in notes" :key="note.id" class="border p-4">
          <h2 class="font-semibold">{{ note.title }}</h2>
          <p>{{ note.content }}</p>
          <button @click="deleteNote(note.id)" class="bg-red-500 text-white p-2 mt-2 rounded">Delete</button>
          <button @click="editNote(note)" class="bg-yellow-500 text-white p-2 mt-2 rounded">Edit</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        notes: [],
        newNote: {
          title: '',
          content: '',
        },
        editingNote: null,
      };
    },
    created() {
      this.getNotes();
    },
    methods: {
      // Fetch all notes from the backend API
      async getNotes() {
        try {
          const response = await axios.get('https://http://localhost:5173//api/notes'); 
          this.notes = response.data;
        } catch (error) {
          console.error('There was an error fetching the notes:', error);
        }
      },
      
      // Create a new note
      async createNote() {
        try {
          const response = await axios.post('https://http://localhost:5173//api/notes', this.newNote);
          this.notes.push(response.data);
          this.newNote.title = '';
          this.newNote.content = '';
        } catch (error) {
          console.error('There was an error creating the note:', error);
        }
      },
  
      // Delete a note by ID
      async deleteNote(id) {
        try {
          await axios.delete(`https://http://localhost:5173//api/notes/${id}`); 
          this.notes = this.notes.filter(note => note.id !== id);
        } catch (error) {
          console.error('There was an error deleting the note:', error);
        }
      },
  
      // Edit a note (set it to the editing state)
      editNote(note) {
        this.editingNote = { ...note };
      },
  
      // Update an existing note
      async updateNote() {
        if (!this.editingNote) return;
  
        try {
          await axios.put(`https://http://localhost:5173//api/notes/${this.editingNote.id}`, this.editingNote);
          this.notes = this.notes.map(note => note.id === this.editingNote.id ? this.editingNote : note);
          this.editingNote = null;
        } catch (error) {
          console.error('There was an error updating the note:', error);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .container {
    max-width: 600px;
  }
  </style>
  