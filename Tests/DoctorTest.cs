using Domain.Logic.Interfaces;

namespace Tests
{
    public class DoctorTest
    {
        private readonly DoctorInteractor _service;
        private readonly Mock<IDoctorRepository> _mock;
        private readonly Mock<IAppointmentRepository> __mock;

        public DoctorTest()
        {
            _mock = new Mock<IDoctorRepository>();
            __mock = new Mock<IAppointmentRepository>();
            _service = new DoctorInteractor(_mock.Object, __mock.Object);
        }

        [Fact]
        public void Create_Invalid()
        {
            var doctor = new Doctor();
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor", result.Error);
        }

        [Fact]
        public void Create_CreateError()
        {
            _mock.Setup(repository => repository.createDoctor(It.IsAny<Doctor>())).Returns(() => false);
            var doctor = new Doctor(0, "a", new Specialization(1, "a"));
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.isFailure);
            Assert.Equal("Cannot create doctor", result.Error);
        }

        [Fact]
        public void Create_Valid()
        {
            _mock.Setup(repository => repository.createDoctor(It.IsAny<Doctor>())).Returns(() => true);
            var doctor = new Doctor(0, "a", new Specialization(1, "a"));
            var result = _service.CreateDoctor(doctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void Delete_IdNotFound()
        {
            List<Appointment> Appointments = new();

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_AppointmentsNotEmpty()
        {
            __mock.Setup(repository => repository.CreateAppointment(It.IsAny<Appointment>())).Returns(() => true);


            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Equal("Cannot delete doctor. Doctor has appointments", result.Error);
        }

        [Fact]
        public void Delete_DoctorNotFound()
        {
            List<Appointment> Appointments = new()
            {
                new Appointment()
            };
            _mock.Setup(repository => repository.getDoctor(It.IsAny<int>())).Returns(() => null);

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_DeleteError()
        {
            List<Appointment> Appointments = new();
            _mock.Setup(repository => repository.getDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));
            _mock.Setup(repository => repository.deleteDoctor(It.IsAny<int>())).Returns(() => false);

            var result = _service.DeleteDoctor(0);

            Assert.True(result.isFailure);
            Assert.Equal("Cannot delete the doctor", result.Error);
        }

        [Fact]
        public void Delete_Valid()
        {
            List<Appointment> Appointments = new();
            _mock.Setup(repository => repository.getDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));
            _mock.Setup(repository => repository.deleteDoctor(It.IsAny<int>())).Returns(() => true);

            var result = _service.DeleteDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetById_Invalid()
        {
            var result = _service.GetDoctor(-1);

            Assert.True(result.isFailure);
            Assert.Equal("Incorrect doctor id", result.Error);
        }

        [Fact]
        public void GetById_NotFound()
        {
            _mock.Setup(repository => repository.getDoctor(It.IsAny<int>())).Returns(() => null);

            var result = _service.GetDoctor(0);

            Assert.True(result.isFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void GetById_Valid()
        {
            _mock.Setup(repository => repository.getDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));

            var result = _service.GetDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetBySpec_Invalid()
        {
            var result = _service.GetDoctor(new Specialization());

            Assert.True(result.isFailure);
            Assert.Contains("Incorrect doctor specialization: ", result.Error);
        }

        [Fact]
        public void GetBySpec_NotFound()
        {
            _mock.Setup(repository => repository.getDoctor(It.IsAny<Specialization>())).Returns(() => null);

            var result = _service.GetDoctor(new Specialization(0, "a"));

            Assert.True(result.isFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void GetBySpec_Valid()
        {
            _mock.Setup(repository => repository.getDoctor(It.IsAny<Specialization>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));

            var result = _service.GetDoctor(new Specialization(0, "a"));

            Assert.True(result.Success);
        }

        [Fact]
        public void EmptyDoctor()
        {
            var doctor = new Doctor();
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Equal("Incorrect doctor id", check.Error);
        }

        [Fact]
        public void NegativeID()
        {
            var doctor = new Doctor(-1, "fullname", new Specialization(0, "a"));
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Equal("Incorrect doctor id", check.Error);
        }

        [Fact]
        public void EmptyFullname()
        {
            var doctor = new Doctor(1, "", new Specialization(0, "a"));
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Equal("Incorrect doctor fullname", check.Error);
        }

        [Fact]
        public void InvalidSpecialization()
        {
            var doctor = new Doctor(0, "fullname", new Specialization());
            var check = doctor.IsValid();

            Assert.True(check.isFailure);
            Assert.Equal("Incorrect specialization", check.Error);
        }

        [Fact]
        public void ValidDoctor()
        {
            var doctor = new Doctor(0, "fullname", new Specialization(0, "name"));
            var check = doctor.IsValid();
            Console.WriteLine(check.Error);

            Assert.True(check.Success);
        }
    }
}