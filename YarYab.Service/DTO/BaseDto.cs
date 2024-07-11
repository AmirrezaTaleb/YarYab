using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YarYab.Domain;
using YarYab.Service.Mapping;

namespace YarYab.Service.DTO
{
    public abstract class BaseDto<TDto, TEntity> : IHaveCustomMapping
        where TDto : class, new()
       where TEntity : class, new()
    {

        public TEntity ToEntity(IMapper mapper)
        {
            return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
        }

        public TEntity ToEntity(IMapper mapper, TEntity entity)
        {
            return mapper.Map(CastToDerivedClass(mapper, this), entity);
        }

        public static TDto FromEntity(IMapper mapper, TEntity model)
        {
            return mapper.Map<TDto>(model);
        }

        public TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity> baseInstance)
        {
            return mapper.Map<TDto>(baseInstance);
        }
        public static TEntity CastDerivedClassToBaseClass(IMapper mapper,TDto dto )
         {
            return  mapper.Map<TEntity>(dto);
         }
        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            //Ignore any property of source (like Post.Author) that dose not contains in destination 
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            CustomMappings(mappingExpression.ReverseMap());
        }

        public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
        {
        }
    }
}
