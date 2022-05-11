using Film_Listing_API.Dtos;

namespace Film_Listing_API.Mapper
{
    public interface IModelFactory
    {
        public void UpdateMovieFactory(Movie dbMovie, UpdateMovieDto updateMovieDto);
        public Movie CreateMovieFactory(CreateMovieDto newMovie);

        public void UpdateActorFactory(Actor dbActor, UpdateActorDto updateActorDto);
        public Actor CreateActorFactory(CreateActorDto newActor);

        public void  UpdateProducerFactory(Producer dbProducer, UpdateProducerDto updateProducerDto);
        public Producer CreateProducerFactory(CreateProducerDto newProducer);
        
    }

    public class ModelFactory : IModelFactory
    {
        public ModelFactory()
        {

        }

        public Movie CreateMovieFactory(CreateMovieDto newMovie)
        {
            return new Movie
            {
                Name = newMovie.Name,
                ReleaseDate = newMovie.ReleaseDate,
                Duration = newMovie.Duration,
                Synopsis = newMovie.Synopsis,
                Image = newMovie.Image,
            };
        }

        public void UpdateMovieFactory(Movie dbMovie, UpdateMovieDto updateMovieDto)
        {
            dbMovie.Name = updateMovieDto.Name;
            dbMovie.ReleaseDate = updateMovieDto.ReleaseDate;
            dbMovie.Duration = updateMovieDto.Duration;
            dbMovie.Synopsis = updateMovieDto.Synopsis;
            dbMovie.Image = updateMovieDto.Image;
        }

        public Actor CreateActorFactory(CreateActorDto newActor)
        {
            return new Actor
            {
                Name = newActor.Name,
                BirthDate = newActor.BirthDate,
                Image = newActor.Image,
            };
        }

        public void UpdateActorFactory(Actor dbActor, UpdateActorDto updateActorDto)
        {
            dbActor.Name = updateActorDto.Name;
            dbActor.BirthDate = updateActorDto.BirthDate;
            dbActor.Image = updateActorDto.Image;
        }

        public Producer CreateProducerFactory(CreateProducerDto newProducer)
        {
            return new Producer
            {
                Name = newProducer.Name,
                FundationDate = newProducer.FundationDate,
                Image = newProducer.Image
            };
        }

        public void UpdateProducerFactory(Producer dbProducer, UpdateProducerDto updateProducerDto)
        {
            dbProducer.Name = updateProducerDto.Name;
            dbProducer.FundationDate = updateProducerDto.FundationDate;
            dbProducer.Image = updateProducerDto.Image;
        }

    }
}
