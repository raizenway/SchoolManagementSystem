# 🏫 School Management System API

Sistem manajemen sekolah sederhana untuk penyelesaian tugas PKL NTI.

## 💻 Techstack
1. .NET 8.0.411
2. PostgreSQL with Neon 
3. Swagger
4. Entity Framework Core

---

## 🚀 How to Run Locally

### 1. Clone Repository

```bash
git clone https://github.com/raizenway/SchoolManagementSystem
cd SchoolManagementSystem
```

### 2. Setup `.env`

Buat file `.env` di root folder dan isi koneksi PostgreSQL:

```env
DB_HOST=your-db-host
DB_PORT=5432
DB_NAME=your-db-name
DB_USER=your-db-user
DB_PASS=your-db-password
```

### 3. Jalankan Migrasi Database

```bash
dotnet ef database update
```

> Pastikan sudah install .NET 8 SDK dan EF Tools

### 4. Jalankan Aplikasi

```bash
dotnet run
```

Akses Swagger di:
```
http://localhost:{port}/swagger
```

---

## 📚 API Descriptions & Example Requests

### 📌 Students

```http
GET /api/Student
```

```http
POST /api/Student
Content-Type: application/json

{
  "fullName": "Saabiq",
  "email": "saabiq@polban.ac.id",
  "dateOfBirth": "2004-10-01T00:00:00"
}
```

```http
PUT /api/Student/4
{
  "fullName": "Saabiq Nur",
  "email": "saabiq.nur@polban.ac.id",
  "dateOfBirth": "2004-10-01T00:00:00"
}
```

```http
DELETE /api/Student/4
```

---

### 👨‍🏫 Teachers

```http
GET /api/Teacher
```

```http
POST /api/Teacher
{
  "fullName": "Yudi Widhiyasana",
  "email": "yudi@polban.ac.id",
  "subject": "Pemrograman"
}
```

---

### 🏫 Classes

```http
GET /api/Class
```

```http
POST /api/Class
{
  "name": "Kelas DDP",
  "teacherId": 1
}
```

---

### 📥 Enrollments

```http
GET /api/Enrollment
```

```http
POST /api/Enrollment?studentId=4&classId=1
```

```http
DELETE /api/Enrollment?studentId=4&classId=1
```

---

## 🧱 Database Setup

- Menggunakan PostgreSQL (Neon/Supabase)
- Gunakan `.env` untuk konfigurasi database
- EF Core otomatis membuat tabel saat `dotnet ef database update` dijalankan

---

## 🧪 Swagger Testing

Swagger UI tersedia di:
```
http://localhost:{PORT}/swagger
```
